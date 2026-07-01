using System.Net;
using System.Net.Http.Json;
using JobPilotAI.Web.Models;

namespace JobPilotAI.Web.Services;

public sealed class ApiClient(HttpClient httpClient)
{
    public async Task<IReadOnlyCollection<SavedJob>> GetJobsAsync(
        CancellationToken cancellationToken = default)
    {
        using var response = await httpClient.GetAsync("/api/jobs", cancellationToken);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<List<SavedJob>>(cancellationToken)
            ?? throw new InvalidOperationException("The API returned an empty response.");
    }

    public async Task<SavedJobDetails?> GetJobAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        using var response = await httpClient.GetAsync($"/api/jobs/{id}", cancellationToken);

        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            return null;
        }

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<SavedJobDetails>(cancellationToken)
            ?? throw new InvalidOperationException("The API returned an empty response.");
    }

    public async Task<ProcessJobResult> ProcessJobAsync(
        ProcessJobRequest request,
        CancellationToken cancellationToken = default)
    {
        using var response = await httpClient.PostAsJsonAsync(
            "/api/jobs/process",
            request,
            cancellationToken);

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<ProcessJobResult>(cancellationToken)
            ?? throw new InvalidOperationException("The API returned an empty response.");
    }
}
