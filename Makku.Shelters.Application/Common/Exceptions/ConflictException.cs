﻿namespace Makku.Shelters.Application.Common.Exceptions
{
    public class ConflictException : Exception
    {
        public ConflictException(string name)
            : base(name) { }

    }
}
