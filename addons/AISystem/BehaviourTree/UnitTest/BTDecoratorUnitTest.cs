namespace GdUnit4.Tests
{
    //using static Assertions;

    [TestSuite]
    public class DecoratorsUnitTest
    {
        [TestCase]
        public void AlwaysFail()
        {
            BTBehaviour.BTStatus status = BTBehaviour.BTStatus.SUCCESS;
            BTAlwaysFail always0 = new BTAlwaysFail();

            // Success Test
            always0.SetLeaf(new BTLeafSuccess());
            status = always0.Tick(0.0f, null, null);
            Assertions.AssertThat(status).IsEqual(BTBehaviour.BTStatus.FAILURE);

            // Failure Test
            always0.SetLeaf(new BTLeafFailure());
            status = always0.Tick(0.0f, null, null);
            Assertions.AssertThat(status).IsEqual(BTBehaviour.BTStatus.FAILURE);

            // Running Test
            always0.SetLeaf(new BTLeafRunning());
            status = always0.Tick(0.0f, null, null);
            Assertions.AssertThat(status).IsEqual(BTBehaviour.BTStatus.RUNNING);
        }

        [TestCase]
        public void AlwaysSuceed()
        {
            BTBehaviour.BTStatus status = BTBehaviour.BTStatus.SUCCESS;
            BTAlwaysSuceed always0 = new BTAlwaysSuceed();

            // Success Test
            always0.SetLeaf(new BTLeafSuccess());
            status = always0.Tick(0.0f, null, null);
            Assertions.AssertThat(status).IsEqual(BTBehaviour.BTStatus.SUCCESS);

            // Failure Test
            always0.SetLeaf(new BTLeafFailure());
            status = always0.Tick(0.0f, null, null);
            Assertions.AssertThat(status).IsEqual(BTBehaviour.BTStatus.SUCCESS);

            // Running Test
            always0.SetLeaf(new BTLeafRunning());
            status = always0.Tick(0.0f, null, null);
            Assertions.AssertThat(status).IsEqual(BTBehaviour.BTStatus.RUNNING);
        }

        [TestCase]
        public void Inverter()
        {
            BTBehaviour.BTStatus status = BTBehaviour.BTStatus.SUCCESS;
            BTInverter invert = new BTInverter();

            // Success Test
            invert.SetLeaf(new BTLeafSuccess());
            status = invert.Tick(0.0f, null, null);
            Assertions.AssertThat(status).IsEqual(BTBehaviour.BTStatus.FAILURE);

            // Failure Test
            invert.SetLeaf(new BTLeafFailure());
            status = invert.Tick(0.0f, null, null);
            Assertions.AssertThat(status).IsEqual(BTBehaviour.BTStatus.SUCCESS);

            // Running Test
            invert.SetLeaf(new BTLeafRunning());
            status = invert.Tick(0.0f, null, null);
            Assertions.AssertThat(status).IsEqual(BTBehaviour.BTStatus.RUNNING);
        }

    }
}