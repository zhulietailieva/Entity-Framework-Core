﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boardgames.Common
{
    public class ValidationConstants
    {
        //boardgame
        public const int BoardgameNameMinLength = 10;
        public const int BoardgameNameMaxLength = 20;

        public const double BoardgameRatingMinValue = 1.00;
        public const double BoardgameRatingMaxValue = 10.00;

        public const int BoardgameYearMinValue = 2018;
        public const int BoardgameYearMaxValue = 2023;

        public const int BoardgameCategoryTypeMinValue = 0;
        public const int BoardgameCategoryTypeMaxValue = 4;

        //Seller
        public const int SellerNameMinLength = 5;   
        public const int SellerNameMaxLength = 20;

        public const int SellerAddressMinLength = 2;
        public const int SellerAddressMaxLength = 30;

        public const string SellerWebsiteRegEx = @"www.[A-Za-z\d-]+.com";

        //Creator
        public const int CreatorFirstNameMinLength = 2;
        public const int CreatorFirstNameMaxLength = 7;

        public const int CreatorLastNameMinLength = 2;
        public const int CreatorLastNameMaxLength = 7;
    }
}
