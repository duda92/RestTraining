using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace RestTraining.Common.DTO
{
    public class HotelsAttractionDTO
    {
        public HotelsAttractionTypeDTO HotelsAttractionType { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Invalid count of hotel's acttraction")]
        public int Count { get; set; }
    }
}
