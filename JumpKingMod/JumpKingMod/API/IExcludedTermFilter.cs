﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JumpKingMod.API
{
    /// <summary>
    /// An interface representing a filter which can check against an exclusion list
    /// </summary>
    public interface IExcludedTermFilter
    {
        /// <summary>
        /// Returns whether the text to check contains an excluded term or not
        /// </summary>
        bool ContainsExcludedTerm(string textToCheck);
    }
}
