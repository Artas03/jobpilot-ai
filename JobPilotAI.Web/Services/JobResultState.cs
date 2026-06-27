using JobPilotAI.Web.Models;

namespace JobPilotAI.Web.Services;

public sealed class JobResultState
{
    public ProcessJobResult? Current { get; private set; }

    public void Set(ProcessJobResult result)
    {
        ArgumentNullException.ThrowIfNull(result);
        Current = result;
    }
}
