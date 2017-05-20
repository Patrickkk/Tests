namespace FileEtl.Core
{
    public interface IEtlStep
    {
        //string Name { get; set; }
    }

    public static class EtlStepExtensions
    {
        //public static void Match(this IEtlStep step, Action<IEtlStep> withoutInput, Action<IEtlStep> withInput)
        //{
        //    if (step.GetType().ImplementsOpenGenericInterface(typeof(IEtlStep<>)))
        //    {
        //        withoutInput(step);
        //    }
        //    else if (step.GetType().ImplementsOpenGenericInterface(typeof(IEtlStep<,>)))
        //    {
        //        withInput(step);
        //    }
        //    else
        //    {
        //        throw new Exception("Unknown step type");
        //    }
        //}
    }
}