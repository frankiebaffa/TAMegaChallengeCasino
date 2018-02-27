using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TAMegaChallengeCasino
{
    public partial class _default : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                double playerMoney = 100;
                double betAmount = 0;
                ViewState.Add("money", playerMoney);
                ViewState.Add("bet", betAmount);
                playerMoney = (double)ViewState["money"];
                betAmount = (double)ViewState["bet"];

                //  randomize images
                imageRandom();

                // print playerMoney
                printStatus();
            }
        }

        protected void leverButton_Click(object sender, EventArgs e)
        {
            pullLever();
        }


        //  METHODS

        //  randomize three images
        private void imageRandom()
        {
            //  array of slot images
            string[] imageNames = { "img/Bar.png", "img/Bell.png","img/Cherry.png","img/Clover.png",
                "img/Diamond.png","img/HorseShoe.png","img/Lemon.png","img/Orange.png","img/Plum.png",
                "img/Seven.png","img/Strawberry.png","img/Watermelon.png"};

            // array of integers
            int[] randomInt = { 0, 0, 0 };

            //  randomize array of integers
            Random random = new Random();
            for (int i = 0; i < randomInt.Length; i++)
            {
                randomInt[i] += random.Next(0, 11);
            }

            //  assign img names
            oneImg.ImageUrl = String.Format("{0}", imageNames[randomInt[0]]);
            twoImg.ImageUrl = String.Format("{0}", imageNames[randomInt[1]]);
            threeImg.ImageUrl = String.Format("{0}", imageNames[randomInt[2]]);

        }

        //  take playerMoney from ViewState and display as currency
        //  in statusLabel
        private void printStatus()
        {
            statusLabel.Text = String.Format(
                    "Player money: {0:C}",
                    ViewState["money"]);
        }

        private void pullLever()
        {
            // deduct money from betBox
            double playerMoney = (double)ViewState["money"];

            //  make sure proper value is in betBox
            double betAmount = double.Parse(betBox.Text);

            ViewState.Add("bet", betAmount);

            ViewState["bet"] = betAmount;

            playerMoney -= betAmount;
            ViewState["money"] = playerMoney;

            // randomize images
            imageRandom();

            //  check image results
            if (checkBar())
            {
                doBar();
            }
            else if (checkJackpot())
            {
                doJackpot();
            }
            else
            {
                doCherry();
            }

            printResult();
            printStatus();
        }

        private void doBar()
        {
            ViewState["bet"] = 0;
        }

        private void doJackpot()
        {
            double betAmount = (double)ViewState["bet"] * 100;
            double playerMoney = (double)ViewState["money"] + betAmount;
            ViewState["money"] = playerMoney;
            ViewState["bet"] = betAmount;
        }

        private void doCherry()
        {
            int cherryMultiplier = 0;
            cherryMultiplier += checkCherry();
            double betAmount = (double)ViewState["bet"] * cherryMultiplier;
            double playerMoney = (double)ViewState["money"] + betAmount;
            ViewState["money"] = playerMoney;
            ViewState["bet"] = betAmount;
        }

        //  Print winnings, if any
        private void printResult()
        {
            /*
            double betAmount = (double)ViewState["bet"];
            if (betAmount > 0)
            {
                resultLabel.Text = String.Format(
                    "You won {0:C}",
                    betAmount);
            }
            else
            {
                resultLabel.Text = String.Format(
                    "You won nothing.");
            }
            */
            resultLabel.Text = String.Format(
                "You won {0:C}",
                ViewState["bet"]);
        }

        //  check photos for Bar - return true or false
        private bool checkBar()
        {
            if (oneImg.ImageUrl == "img/Bar.png"
                || twoImg.ImageUrl == "img/Bar.png"
                || threeImg.ImageUrl == "img/Bar.png")
                return true;
            else
                return false;
        }

        //  check photos for Jackpot - return true or false
        private bool checkJackpot()
        {
            if (oneImg.ImageUrl == "img/Seven.png"
                && twoImg.ImageUrl == "img/Seven.png"
                && threeImg.ImageUrl == "img/Seven.png")
                return true;
            else
                return false;
        }

        //  check photos for Cherries - return the multiplication factor
        private int checkCherry()
        {
            int cherryCount = 0;
            int cherryFactor = 0;
            string[] imageArray = { oneImg.ImageUrl, twoImg.ImageUrl, threeImg.ImageUrl };
            for (int i = 0; i < imageArray.Length; i++)
                if (imageArray[i] == "img/Cherry.png")
                    cherryCount += 1;
            if (cherryCount == 1) cherryFactor = 2;

            else if (cherryCount == 2) cherryFactor = 3;

            else if (cherryCount == 3) cherryFactor = 4;

            return cherryFactor;
        }
    }
}