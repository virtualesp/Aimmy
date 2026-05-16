using Microsoft.ML.OnnxRuntime;

namespace Aimmy2.AILogic
{
    internal static class OnnxModelSessionFactory
    {
        public static SessionOptions CreateDefaultOptions()
        {
            return new SessionOptions
            {
                EnableCpuMemArena = true,
                EnableMemoryPattern = false,
                GraphOptimizationLevel = GraphOptimizationLevel.ORT_ENABLE_ALL,
                ExecutionMode = ExecutionMode.ORT_SEQUENTIAL,
                InterOpNumThreads = 1,
                IntraOpNumThreads = 4
            };
        }

        public static OnnxModelLoadResult Load(string modelPath, SessionOptions sessionOptions, bool useDirectML)
        {
            if (useDirectML) { sessionOptions.AppendExecutionProvider_DML(); }
            else { sessionOptions.AppendExecutionProvider_CPU(); }

            var session = new InferenceSession(modelPath, sessionOptions);
            return new OnnxModelLoadResult(session, new List<string>(session.OutputMetadata.Keys));
        }
    }

    internal sealed record OnnxModelLoadResult(InferenceSession Session, List<string> OutputNames);
}
