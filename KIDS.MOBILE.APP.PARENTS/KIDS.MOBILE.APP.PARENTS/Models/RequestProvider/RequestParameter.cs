﻿namespace KIDS.MOBILE.APP.PARENTS.Models.RequestProvider
{
    public class RequestParameter
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public RequestParameter(string key,string value)
        {
            Key = key;
            Value = value;
        }
    }
}
