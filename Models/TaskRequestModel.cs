using System.Text.Json.Serialization;

namespace Api.SystemTests.Models;

public class TaskRequestModel
{
    [JsonPropertyName("strorage_id"), JsonRequired, JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string StorageId { get; set; } = default!;

    [JsonPropertyName("reference_id"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? ReferenceId { get; set; }

    [JsonPropertyName("correlation_id"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? CorrelationId { get; set; }

    [JsonPropertyName("causation_id"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? CausationId { get; set; }

    [JsonPropertyName("origin"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Origin { get; set; }

    [JsonPropertyName("title"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Title { get; set; }

    [JsonPropertyName("description"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Description { get; set; }

    [JsonPropertyName("icon"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Icon { get; set; }

    [JsonPropertyName("related_entity"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? RelatedEntity { get; set; }

    [JsonPropertyName("priority"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public int Priority { get; set; }

    [JsonPropertyName("percent_complete"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public int PercentComplete { get; set; }

    [JsonPropertyName("possible_outcomes"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<string>? PossibleOutcomes { get; set; }

    [JsonPropertyName("chosen_outcome"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? ChosenOutcome { get; set; }

    [JsonPropertyName("assigned_to"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? AssignedTo { get; set; }

    [JsonPropertyName("start_date_time"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? StartDateTime { get; set; }

    [JsonPropertyName("due_date_time"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? DueDateTime { get; set; }

    [JsonPropertyName("require_chosen_outcome"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? RequireChosenOutcome { get; set; }

    [JsonPropertyName("reviewers"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<string>? Reviewers { get; set; }

    [JsonPropertyName("minimum_reviewers"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? MinimumReviewers { get; set; }

    [JsonPropertyName("due_date_offset"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public DueDateOffset? DateOffset { get; set; }

    public class DueDateOffset
    {
        [JsonPropertyName("number_of_days"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? NumberOfDays { get; set; }

        [JsonPropertyName("offset_type"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? OffsetType { get; set; }
    }
}
