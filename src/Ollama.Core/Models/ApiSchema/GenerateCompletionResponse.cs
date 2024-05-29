namespace Ollama.Core.Models;


public class GenerateCompletionResponse
{
    public string model { get; set; }
    public DateTime created_at { get; set; }
    public string response { get; set; }
    public bool done { get; set; }
    public long[] context { get; set; }
    public long total_duration { get; set; }
    public int load_duration { get; set; }
    public int prompt_eval_count { get; set; }
    public int prompt_eval_duration { get; set; }
    public int eval_count { get; set; }
    public long eval_duration { get; set; }
}
