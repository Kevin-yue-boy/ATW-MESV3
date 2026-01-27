using SqlSugar;

namespace ATW.MES.Model.Test
{
    public class Collect_DAQMain
    {
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsNullable =false, IsPrimaryKey = true, IsIdentity = true)]
        public long Id { get; set; }

        /// <summary>
        /// Desc:GUID
        /// Default:newid()
        /// Nullable:False
        /// </summary>        
        [SugarColumn(IsNullable = false, ColumnDescription = "GUID")]
        public Guid GUID { get; set; }

        /// <summary>
        /// Desc:产线编号
        /// Default:
        /// Nullable:False
        /// </summary>      
        [SugarColumn(IsNullable = false, ColumnDescription = "产线编号", Length = 100)]
        public string LineCode { get; set; }

        /// <summary>
        /// Desc:计划编号
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsNullable = false,  ColumnDescription = "计划编号", Length = 100)]
        public string PlanNo { get; set; }

        /// <summary>
        /// Desc:产品类型
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsNullable = false, ColumnDescription = "产品类型", Length = 100)]
        public string ProductType { get; set; }

        /// <summary>
        /// Desc:产品编码
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsNullable = false,ColumnDescription = "产品编码", Length = 100)]
        public string ProductCode { get; set; }

        /// <summary>
        /// Desc:流水号
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(IsNullable = true, ColumnDescription = "流水号")]
        public long SN { get; set; }

        /// <summary>
        /// Desc:产品码值
        /// Default:
        /// Nullable:True
        /// </summary>      
        [SugarColumn(IsNullable = true, ColumnDescription = "产品码值", Length = 150)]
        public string Code { get; set; }

        /// <summary>
        /// Desc:档位
        /// Default:
        /// Nullable:True
        /// </summary>          
        [SugarColumn(IsNullable = true, ColumnDescription = "档位", Length = 50)]
        public string Grade { get; set; }

        /// <summary>
        /// Desc:工装编号
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(IsNullable = true, ColumnDescription = "工装编号", Length = 50)]
        public string TemplateNo { get; set; }

        /// <summary>
        /// Desc:产品状态
        /// Default:
        /// Nullable:True
        /// </summary>    
        [SugarColumn(IsNullable = true, ColumnDescription = "产品状态", Length = 50)]
        public string ProductState { get; set; }

        /// <summary>
        /// Desc:工序状态
        /// Default:
        /// Nullable:False
        /// </summary>    
        [SugarColumn(IsNullable = true, ColumnDescription = "工序状态", Length = 50)]
        public string ProcessState { get; set; }

        /// <summary>
        /// Desc:工序名称
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsNullable = false, ColumnDescription = "工序名称", Length = 200)]
        public string ProcessName { get; set; }

        /// <summary>
        /// Desc:工序编码
        /// Default:
        /// Nullable:False
        /// </summary>          
        [SugarColumn(IsNullable = false, ColumnDescription = "工序编码", Length = 200)]
        public string ProcessCode { get; set; }

        /// <summary>
        /// Desc:是否返修
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsNullable = false, ColumnDescription = "是否返修")]
        public bool RepairYN { get; set; }

        /// <summary>
        /// Desc:是否过时
        /// Default:
        /// Nullable:false
        /// </summary>           
        [SugarColumn(IsNullable = false, ColumnDescription = "是否过时")]
        public bool LogoutYN { get; set; }

        /// <summary>
        /// Desc:入站时间
        /// Default:
        /// Nullable:True
        /// </summary>      
        [SugarColumn(IsNullable = true, ColumnDescription = "入站时间")]
        public DateTime? CheckInTime { get; set; }

        /// <summary>
        /// Desc:出站时间
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(IsNullable = true, ColumnDescription = "出站时间")]
        public DateTime? CheckOutTime { get; set; }

        /// <summary>
        /// Desc:创建时间
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(IsNullable = true, ColumnDescription = "创建时间")]
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// Desc:更新时间
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(IsNullable = true, ColumnDescription = "更新时间")]
        public DateTime? LastEditTime { get; set; }

        /// <summary>
        /// Desc:备注
        /// Default:
        /// Nullable:True
        /// </summary>       
        [SugarColumn(IsNullable = true, ColumnDescription = "备注", ColumnDataType = "NVARCHAR(max)")]
        public string Remark { get; set; }

        /// <summary>
        /// Desc:产品在工装位置
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(IsNullable = true, ColumnDescription = "产品在工装位置", Length =10)]
        public string TemplateLocation { get; set; }

        /// <summary>
        /// Desc:子产品类型
        /// Default:0
        /// Nullable:True
        /// </summary>           
        [SugarColumn(IsNullable = true, ColumnDescription = "子产品类型")]
        public int? Type { get; set; }

        /// <summary>
        /// Desc:子产品数量
        /// Default:0
        /// Nullable:True
        /// </summary>         
        [SugarColumn(IsNullable = true, ColumnDescription = "模组产品类型")]
        public int? SubCount { get; set; }


    }
}
