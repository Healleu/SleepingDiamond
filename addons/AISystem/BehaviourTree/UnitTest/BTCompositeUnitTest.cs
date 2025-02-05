namespace GdUnit4.Tests
{
    //using static Assertions;

    [TestSuite]
    public class CompositesUnitTest
    {
        [TestCase]
        public void Sequence()
        {
            // Success Test
            BTSequence sequence = new BTSequence();
            sequence.AddLeaf(new BTLeafSuccess());
            sequence.AddLeaf(new BTLeafSuccess());
            sequence.AddLeaf(new BTLeafSuccess());
            BTBehaviour.BTStatus status = sequence.Tick(0.0f, null, null);
            Assertions.AssertThat(status).IsEqual(BTBehaviour.BTStatus.SUCCESS);
            Assertions.AssertInt(sequence.GetActiveIndex()).IsEqual(0);
            // Failure Test

            BTSequence sequence1 = new BTSequence();
            sequence1.AddLeaf(new BTLeafSuccess());
            sequence1.AddLeaf(new BTLeafFailure());
            sequence1.AddLeaf(new BTLeafSuccess());
            BTBehaviour.BTStatus status1 = sequence1.Tick(0.0f, null, null);
            Assertions.AssertThat(status1).IsEqual(BTBehaviour.BTStatus.FAILURE);
            Assertions.AssertInt(sequence1.GetActiveIndex()).IsEqual(0);

            // Running Test
            BTSequence sequence2 = new BTSequence();
            sequence2.AddLeaf(new BTLeafSuccess());
            sequence2.AddLeaf(new BTLeafRunning());
            sequence2.AddLeaf(new BTLeafFailure());
            BTBehaviour.BTStatus status2 = sequence2.Tick(0.0f, null, null);
            Assertions.AssertThat(status2).IsEqual(BTBehaviour.BTStatus.RUNNING);
            Assertions.AssertInt(sequence2.GetActiveIndex()).IsEqual(1);
        }


        [TestCase]
        public void SequenceStep()
        {
            // 1 Success Test
            BTSequenceStep sequence = new BTSequenceStep();
            sequence.AddLeaf(new BTLeafSuccess());
            sequence.AddLeaf(new BTLeafSuccess());
            sequence.AddLeaf(new BTLeafSuccess());
            BTBehaviour.BTStatus status = sequence.Tick(0.0f, null, null);
            Assertions.AssertThat(status).IsEqual(BTBehaviour.BTStatus.RUNNING);
            Assertions.AssertInt(sequence.GetActiveIndex()).IsEqual(1);

            // Full Test
            BTSequenceStep sequence5 = new BTSequenceStep();
            sequence5.AddLeaf(new BTLeafSuccess());
            sequence5.AddLeaf(new BTLeafSuccess());
            sequence5.AddLeaf(new BTLeafSuccess());
            sequence5.Tick(0.0f, null, null);
            sequence5.Tick(0.0f, null, null);
            BTBehaviour.BTStatus status5 = sequence5.Tick(0.0f, null, null);
            Assertions.AssertThat(status5).IsEqual(BTBehaviour.BTStatus.SUCCESS);
            Assertions.AssertInt(sequence5.GetActiveIndex()).IsEqual(0);

            // Failure Test
            BTSequenceStep sequence1 = new BTSequenceStep();
            sequence1.AddLeaf(new BTLeafFailure());
            sequence1.AddLeaf(new BTLeafSuccess());
            sequence1.AddLeaf(new BTLeafSuccess());
            BTBehaviour.BTStatus status1 = sequence1.Tick(0.0f, null, null);
            Assertions.AssertThat(status1).IsEqual(BTBehaviour.BTStatus.FAILURE);
            Assertions.AssertInt(sequence1.GetActiveIndex()).IsEqual(0);

            // Running Test
            BTSequenceStep sequence2 = new BTSequenceStep();
            sequence2.AddLeaf(new BTLeafRunning());
            sequence2.AddLeaf(new BTLeafFailure());
            sequence2.AddLeaf(new BTLeafSuccess());
            BTBehaviour.BTStatus status2 = sequence2.Tick(0.0f, null, null);
            Assertions.AssertThat(status2).IsEqual(BTBehaviour.BTStatus.RUNNING);
            Assertions.AssertInt(sequence2.GetActiveIndex()).IsEqual(0);
        }

        [TestCase]
        public void Selector()
        {

            // Failure Test
            BTSelector sequence = new BTSelector();
            sequence.AddLeaf(new BTLeafFailure());
            sequence.AddLeaf(new BTLeafFailure());
            sequence.AddLeaf(new BTLeafFailure());
            BTBehaviour.BTStatus status = sequence.Tick(0.0f, null, null);
            Assertions.AssertThat(status).IsEqual(BTBehaviour.BTStatus.FAILURE);
            Assertions.AssertInt(sequence.GetActiveIndex()).IsEqual(0);

            // Success Test
            BTSelector sequence1 = new BTSelector();
            sequence1.AddLeaf(new BTLeafFailure());
            sequence1.AddLeaf(new BTLeafSuccess());
            sequence1.AddLeaf(new BTLeafRunning());
            BTBehaviour.BTStatus status1 = sequence1.Tick(0.0f, null, null);
            Assertions.AssertThat(status1).IsEqual(BTBehaviour.BTStatus.SUCCESS);
            Assertions.AssertInt(sequence1.GetActiveIndex()).IsEqual(0);

            // Running Test
            BTSelector sequence2 = new BTSelector();
            sequence2.AddLeaf(new BTLeafFailure());
            sequence2.AddLeaf(new BTLeafRunning());
            sequence2.AddLeaf(new BTLeafSuccess());
            BTBehaviour.BTStatus status2 = sequence2.Tick(0.0f, null, null);
            Assertions.AssertThat(status2).IsEqual(BTBehaviour.BTStatus.RUNNING);
            Assertions.AssertInt(sequence2.GetActiveIndex()).IsEqual(1);

        }


    }
}