﻿namespace newTestApi.Models.DTO
{
    public class AddMarksRequestDto
    {
        public string StudentRegNo { get; set; }
        public string Physics { get; set; }
        public string Chemistry { get; set; }
        public string Maths { get; set; }
        public Boolean Pass { get; set; }
    }
}
