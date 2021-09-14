using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

    namespace restSkylabs.Model
{
    [Table("records")]
    public class Records
    {
        [Key]
        [Required]
        public int ID_Records { get; set; }

        public int Age { get; set; }

        public int Workclass_ID { get; set; }

        public int Education_Level_ID { get; set; }

        public int Education_Num { get; set; }

        public int Marital_Status_ID { get; set; }

        public int Occupation_ID { get; set; }

        public int Relationship_ID { get; set; }

        public int Race_ID { get; set; }

        public int Sex_ID { get; set; }

        public int Capital_Gain { get; set; }

        public int Capital_Loss { get; set; }

        public int Hours_week { get; set; }

        public int Country_ID { get; set; }

        public bool Over_50k { get; set; }

        [ForeignKey("Workclass_ID")]
        public Workclasses Workclasses { get; set; }

        [ForeignKey("Education_Level_ID")]
        public Education_Levels Education_Levels { get; set; }

        [ForeignKey("Marital_Status_ID")]
        public Marital_Statuses Marital_Statuses { get; set; }

        [ForeignKey("Occupation_ID")]
        public Occupations Occupations { get; set; }

        [ForeignKey("Relationship_ID")]
        public Relationships Relationships { get; set; }

        [ForeignKey("Race_ID")]
        public Races Races { get; set; }

        [ForeignKey("Sex_ID")]
        public Sexes Sexes { get; set; }

        [ForeignKey("Country_ID")]
        public Countries Countries { get; set; }

    }


    [Table("workclasses")]
    public class Workclasses
    {
        [Key]
        [Required]
        public int ID_Workclasses { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

    }


    [Table("education_levels")]
    public class Education_Levels
    {
        [Key]
        [Required]
        public int ID_Education_Levels { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

    }

    [Table("marital_statuses")]
    public class Marital_Statuses
    {
        [Key]
        [Required]
        public int ID_Marital_Statuses { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

    }


    [Table("occupations")]
    public class Occupations
    {
        [Key]
        [Required]
        public int ID_Occupations { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

    }

    [Table("relationships")]
    public class Relationships
    {
        [Key]
        [Required]
        public int ID_Relationships { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

    }

    [Table("races")]
    public class Races
    {
        [Key]
        [Required]
        public int ID_Races { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

    }

    [Table("sexes")]
    public class Sexes
    {
        [Key]
        [Required]
        public int ID_Sexes { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

    }

    [Table("countries")]
    public class Countries
    {
        [Key]
        [Required]
        public int ID_Countries { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

    }

}