using Agency.RepositoryInterface;
using ServiceStack.DataAnnotations;
using System;

namespace Agency.Repository
{
    [Alias("tbl_operator_agent")]
    public class OperatorAgent : IOperatorAgent
    {
        [AutoIncrement] 
        [Alias("id")] 
        public int Id { get; set; }
        [Alias("operator_id")] 
        public int OperatorId { get; set; }
        [Alias("agent_id")] 
        public int AgentId { get; set; }
        [Alias("type")] 
        public int Type { get; set; }
        [StringLength(128)] 
        [Alias("code")] 
        public string Code { get; set; }
        [StringLength(256)] 
        [Alias("name")] 
        public string Name { get; set; }
        [Alias("status")] 
        public int Status { get; set; }
        [Alias("is_prg_created_date")] 
        public DateTime IsPrgCreatedDate { get; set; }
        [Alias("is_prg_updated_date")] 
        public DateTime IsPrgUpdatedDate { get; set; }
        [Alias("is_prg_created_user_id")] 
        public int IsPrgCreatedUserId { get; set; }
        [Alias("is_prg_updated_user_id")] 
        public int IsPrgUpdatedUserId { get; set; }
    }
}