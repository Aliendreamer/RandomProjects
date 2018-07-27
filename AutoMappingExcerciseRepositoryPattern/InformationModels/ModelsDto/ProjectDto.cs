namespace InformationModels.ModelsDto
{
    using System;
    using System.Collections.Generic;

    public  class ProjectDto
    {

        public int ProjectId { get; set; }

        public string Name { get; set; }

        public string Goal { get; set; }

        public DateTime StartDate { get; set; }    

        public virtual Employee Manager   { get; set; }

    }
}
