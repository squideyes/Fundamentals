﻿// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

namespace SquidEyes.Fundamentals;

public interface IConfigValue
{
    bool IsValid { get; }
    ConfigValueStatus Status { get; }
    Tag Tag { get; }
}