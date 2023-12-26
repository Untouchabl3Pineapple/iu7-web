using TestsCore.Builders;

namespace TestsCore.ObjectMothers
{
    public class ButtonsPostsOM
    {
        public ButtonsPostsBuilder CreateButtonsPosts()
        {
            return new ButtonsPostsBuilder()
                        .withPost(1)
                        .withLeftSide(1)
                        .withRightSide(1)
                        .withLeftColor("RED")
                        .withRightColor("RED");
        }

        public List<ButtonsPostsBuilder> CreateRange()
        {
            return new List<ButtonsPostsBuilder> {
                        new ButtonsPostsBuilder().withPost(1)
                                                 .withLeftSide(1)
                                                 .withRightSide(1)
                                                 .withLeftColor("RED")
                                                 .withRightColor("RED"),
                        new ButtonsPostsBuilder().withPost(2)
                                                 .withLeftSide(1)
                                                 .withRightSide(1)
                                                 .withLeftColor("RED")
                                                 .withRightColor("RED")
            };
        }
    }
}