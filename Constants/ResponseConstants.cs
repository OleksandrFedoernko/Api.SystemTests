namespace Api.SystemTests.Constants;

public static class ResponseConstants
{
    public static class StorageResponse
    {
        public const string StorageId = "storage_id";
        public const string Name = "name";
        public const string Icon = "icon";
    }

    public static class UserResponse
    {
        public const string UserId = "user_id";
        public const string Name = "name";
        public const string Description = "description";
    }

    public static class TaskResponse
    {
        public const string TaskId = "task_id";
        public const string StorageId = "storage_id";
        public const string StartDateTime = "start_date_time";
        public const string DueDateTime = "due_date_time";
        public const string Priority = "priority";
        public const string PercentComplete = "percent_complete";
        public const string CompletedDateTime = "completed_date_time";
        public const string CreatedBy = "created_by";
        public const string Overdue = "overdue";
    }

    public static class ErrorResponse
    {
        public const string Status = "status";
        public const string Message = "message";
        public const string ValidationMessages = "validation_messages";
        public const string Field = "field";
        public const string ErrorCode = "error_code";
    }

    public static class PaginationResponse
    {
        public const string Limit = "limit";
        public const string Total = "total";
        public const string Items = "items";
        public const string Pagination = "pagination";
    }

    public static class TaskHistoryResponse
    {
        public const string Changes = "changes";
        public const string TaskId = "task_id";
        public const string ChangeId = "change_id";
        public const string ChangedByUserId = "changed_by_user_id";
        public const string ChangedByUser = "changed_by_user";
        public const string ChangedByUserType = "changed_by_user_type";
        public const string RecordedAt = "recorded_at";
    }
}

