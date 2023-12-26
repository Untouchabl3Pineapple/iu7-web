using System.ComponentModel.DataAnnotations;
using db_cp.Models;
using db_cp.ModelsBL;

namespace TestsCore.Builders
{
    public class ButtonsPostsBuilder
    {
        public int Post;
        public short LeftSide;
        public short RightSide;
        public string LeftColor;
        public string RightColor;

        public ButtonsPostsBuilder() { }

        public ButtonsPostsBuilder withPost(int post)
        {
            this.Post = post;
            return this;
        }

        public ButtonsPostsBuilder withLeftSide(short leftSide)
        {
            this.LeftSide = leftSide;
            return this;
        }

        public ButtonsPostsBuilder withRightSide(short rightSide)
        {
            this.RightSide = rightSide;
            return this;
        }

        public ButtonsPostsBuilder withLeftColor(string leftColor)
        {
            this.LeftColor = leftColor;
            return this;
        }

        public ButtonsPostsBuilder withRightColor(string rightColor)
        {
            this.RightColor = rightColor;
            return this;
        }

        public ButtonsPosts build()
        {
            return new ButtonsPosts
            {
                Post = Post,
                LeftSide = LeftSide,
                RightSide = RightSide,
                LeftColor = LeftColor,
                RightColor = RightColor
            };
        }

        public ButtonsPostsBL buildBL()
        {
            return new ButtonsPostsBL
            {
                Post = Post,
                LeftSide = LeftSide,
                RightSide = RightSide,
                LeftColor = LeftColor,
                RightColor = RightColor
            };
        }
    }
}
