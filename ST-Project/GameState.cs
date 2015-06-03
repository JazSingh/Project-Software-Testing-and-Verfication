using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST_Project
{
    public class GameState
    {
        Dungeon d;
        Player p;
        int LKP = 0;    // Last-Known-Position of the Player
        int defend = 1; // contains ID of bridge the packs with a Defend-Order need to defend
        public GameManager parent;

        // Constructor for creating a new game
        public GameState(int i, GameManager prt)
        {
            parent = prt;
            d = new Dungeon(i);
            p = new Player();
            if (parent.isLogging())
                d.iAmYourFather(this);
            d.SpawnMonsters();
            DropItems();
            Console.WriteLine(d.ToString());
            Console.WriteLine(p.ToString());
            Console.WriteLine("Total hp monsters: " + d.SumMonsterHealth());
            Console.WriteLine("Total hp player and pots: " + SumPlayerPotsHP());
        }

        public GameState(int i)
        {
            d = new Dungeon(i);
            p = new Player();
            d.SpawnMonsters();
            DropItems();
            Console.WriteLine(d.ToString());
            Console.WriteLine(p.ToString());
            Console.WriteLine("Total hp monsters: " + d.SumMonsterHealth());
            Console.WriteLine("Total hp player and pots: " + SumPlayerPotsHP());
        }

        // Constructor to load in a previous game
        public GameState(Dungeon dungeon, Player player, GameManager gm)
        {
            parent = gm;
            d = dungeon;
            p = player;
            if (parent.isLogging())
                d.iAmYourFather(this);

            d.SpawnMonsters();
            DropItems();
        }

        public GameState(Dungeon dungeon, Player player)
        {
            d = dungeon;
            p = player;
            d.SpawnMonsters();
            DropItems();
        }

        // Constructor for testing
        public GameState(Dungeon d, Player p, bool testconst)
        {
            this.d = d;
            this.p = p;
        }

        // Called after finishing a dungeon. Create a new dungeon and save the state with the new dungeon
        // the new dungeon does NOT contain any monsters or items yet, these will be spawned afterwards
        public void Save(string filename)
        {
            d = new Dungeon(d.difficulty + 1);
            p.save(d, filename);
        }

        // returns the current dungeon
        public Dungeon GetDungeon()
        {
            return d;
        }

        // returns the player
        public Player GetPlayer()
        {
            return p;
        }

        // returns wether a pack retreats from combat or not
        public bool CheckRetreat()
        {
            if (d.CheckRetreat(p.get_position()))
                return true;
            return false;
        }

        // check wether a battle can be fought between the player and a pack in the players current node
        public bool fighting()
        {
            int pos = GetPlayer().get_position();
            if (GetDungeon().GetNode(pos).hasPack())
            {
                return true;
            }
            return false;
        }

        // returns the sum of the player's current HP and the total of health pots scattered around in the dungeon
        private int SumPlayerPotsHP()
        {
            return p.GetHP() + d.SumHealPots();
        }

        // test method for private method SumPlayerPotsHP()
        public int SumPlayerPotsHPTest()
        {
            return SumPlayerPotsHP();
        }

        // Generates a new dungeon level, if the previous difficulty level was 4 or less, the dificulty goes up by one 
        // also spawns monsters and items in the level
        public void NextLevel()
        {
            d = new Dungeon(d.difficulty == 5 ? 5 : d.difficulty + 1);
            d.SpawnMonsters();
            DropItems();
            if (parent.isLogging())
                d.iAmYourFather(this);
        }

        // sets the player position and checks for loot in the current node
        public void SetPosition(int i)
        {
            p.set_position(i);
            if (i/d.interval < d.difficulty && d.nodes[i].IsBridge(d.interval) && !d.overcome[i / d.interval] && i + d.interval < d.dungeonSize) 
                d.GiveDefendOrder((i / d.interval) + 1);
            defend = i + d.interval < d.dungeonSize ? ((i / d.interval) + 1) * d.interval : d.dungeonSize - 1;
            Console.WriteLine("DEFEND {0}", defend);
            CheckItemsFound();
        }

        // gives the player the loot that can be found in the current node 
        public void CheckItemsFound()
        {
            int i = p.get_position();
            List<Item> items = d.GetNode(i).get_Items();
            foreach (Item j in items)
            {
                p.add(j);
                Console.WriteLine("Item gevonden!");
            }
            d.nodes[i].RemoveItems();
        }

        // returns wether the player has reached the last node of the dungeon
        public bool CheckFinished()
        {
            if (p.get_position() == GetDungeon().nodes.Length - 1)
            {
                Console.WriteLine("Reached end node!");
                return true;
            }
            return false;
        }

        // drops items in the dungeon at random positions, depending on certain constrains
        // a health potion only spawns when the sum of the player's HP and the total HP that can be obtained from other potions in the dungeon
        // is lower than the total hp of the monsters in the dungeon
        private void DropItems()
        {
            int i = 1;
            int k = Oracle.GiveNumber(d.difficulty, d.difficulty + 2);

            while (i <= k)
            {
                int r = Oracle.GiveNumber(2);
                switch (r)
                {
                    case 0: d.DropItem(ItemType.MagicScroll); break;
                    case 1: d.DropItem(ItemType.TimeCrystal); break;
                    default: if (SumPlayerPotsHP() < d.SumMonsterHealth()) d.DropItem(ItemType.HealthPotion); break;
                }
                i++;
            }
        }

        // makes the player fight with the current pack and checks if the pack will run away or if it dies.
        public bool Fight()
        {
            int pos = p.get_position();
            p.setLKP(pos); // refresh last-known-position
            Pack pack = d.nodes[pos].popPack();

            Console.WriteLine("before combat-round: " + pack.GetNumMonsters());

            p.doCombatRound(d, pack);

            if (!pack.isDead())
            {
                d.nodes[pos].pushPack(pack);
                if (CheckRetreat())
                {
                    return true;
                }
            }

            else
            {
                Console.WriteLine("Pack is killed.");
                int score = pack.get_Score();
                GivePackReward(score);
                return true;
            }
            Console.WriteLine("after combat-round: " + pack.GetNumMonsters());
            return false;
        }

        // returns wether the player is still alive or not
        public bool PlayerDead()
        {
            return !p.IsAlive();
        }

        // calculates the points earned by the player for killing a pack
        // If the current node is a bridge, these points will be multiplied by the level of the bridge
        private void GivePackReward(int scr)
        {
            int score = scr;
            int nodeId = p.get_position();
            int interval = d.interval;
            if (d.GetNode(nodeId).IsBridge(interval))
            {
                int level = nodeId / interval;
                score *= level;
                Console.WriteLine("BONUS PUNTEN! Bridge Level: " + level + " Score: " + score);
            }
            else
            {
                Console.WriteLine("NORMALE PUNTEN. Score: " + score);
            }
            p.AwardScore(score);
        }

        // test method for private method GivePackReward(int scr)
        public void GivePackRewardTest(int scr)
        {
            GivePackReward(scr);
        }

        // check wether a potion is available in the players item list and remove it
        // if a health potion is available the player will use it, this counts as a turn
        public void UsePotion()
        {
            List<Item> items = p.getItems();
            for (int t = 0; t < items.Count; t++)
            {
                if (items[t].type == ItemType.HealthPotion)
                {
                    PackMoves(); Console.WriteLine("Use potion.");
                    p.use(d, items[t]); items.Remove(items[t]);
                    UpdateTime();
                    break;
                }
            }
        }

        // check wether a crystal is available in the players item list and remove it
        // if a crystal is available the player will use it, this counts as a turn
        public void UseCrystal()
        {
            List<Item> items = p.getItems();
            for (int t = 0; t < items.Count; t++)
            {
                if (items[t].type == ItemType.TimeCrystal)
                {
                    p.use(d, items[t]); items.Remove(items[t]);
                    Hunt(); // Trigger Hunt-Orders!
                    PackMoves();
                    UpdateTime();
                    break;
                }
            }
        }

        // gives certain packs in the game the Order to Hunt the player
        private void Hunt()
        {
            Console.WriteLine("\t\tHUNT!!!");
            d.GiveHuntOrder();
        }

        public void UpdateLKP()
        {
            LKP = p.get_position();
        }

        // check wether a magic scroll is available in the players item list and remove it
        // if a scroll is available the player will use it, this counts as a turn
        // has a small chance to blow up the current node the player is in, the player will be blown to the nearest node from which the end node can be reached
        public bool UseScroll()
        {
            List<Item> items = p.getItems();
            for (int t = 0; t < items.Count; t++)
            {
                if (items[t].type == ItemType.MagicScroll)
                {
                    PackMoves();
                    p.use(d, items[t]); items.Remove(items[t]);
                    UpdateTime();
                    if (Oracle.Decide())
                    {
                        Console.WriteLine("KABOOOOM");
                        int np = d.Destroy(d.nodes[p.get_position()]);
                        Console.WriteLine("Player moves to: " + np);
                        SetPosition(np);
                        return true;
                    }
                    break;
                }
            }
            return false;
        }

        // Update the time for the items that is currently in use
        // moves packs around the dungeon
        public void UpdateTime()
        {
            p.UpdateCurrentItem();
        }

        // Move all packs in the dungeon, depending on the players current location
        public void PackMoves()
        {
            if (parent != null && parent.isLogging())
            {

            }

            d.MovePacks(p.get_position(), LKP, defend);
        }

        public void iAmYourFather(GameManager gm)
        {
            parent = gm;

            d.iAmYourFather(this);
        }
    }
}
