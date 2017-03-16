﻿using Nager.AmazonProductAdvertising.Model;
using System;

namespace Nager.AmazonProductAdvertising.Operation
{
    public class AmazonItemSearchOperation : AmazonOperationBase
    {
        public AmazonItemSearchOperation()
        {
            base.ParameterDictionary.Add("Operation", "ItemSearch");
            base.ParameterDictionary.Add("ResponseGroup", AmazonResponseGroup.Large.ToString());
        }

        public AmazonItemSearchOperation Keywords(string keywords) => AddOrReplace("Keywords", keywords);

        public AmazonItemSearchOperation Skip(int value)
        {
            //http://docs.aws.amazon.com/AWSECommerceService/latest/DG/MaximumNumberofPages.html

            if (value > 10)
            {
                throw new ArgumentOutOfRangeException("value", "value must be between 1 and 5");
            }

            return AddOrReplace("ItemPage", value.ToString());
        }

        public AmazonItemSearchOperation Sort(AmazonSearchSort amazonSearchSort, AmazonSearchSortOrder amazonSearchSortOrder)
        {
            var order = String.Empty;
            if (amazonSearchSortOrder == AmazonSearchSortOrder.Descending)
            {
                order = "-";
            }
            var value = String.Format("{1}{0}", amazonSearchSort.ToString().ToLower(), order);
            return AddOrReplace("Sort", value.ToString());
        }

        public AmazonItemSearchOperation Available() => AddOrReplace("Availability", "Available");

        public AmazonItemSearchOperation Condition(ItemCondition condition) => AddOrReplace("Condition", condition);

        public AmazonItemSearchOperation MaxPrice(int priceInLowestCurrencyDenomination) => AddOrReplace("MaximumPrice", priceInLowestCurrencyDenomination);
        public AmazonItemSearchOperation MinPrice(int priceInLowestCurrencyDenomination) => AddOrReplace("MinimumPrice", priceInLowestCurrencyDenomination);

        public AmazonItemSearchOperation PriceBetween(int maxPriceInLowestCurrencyDenomination, int minPriceInLowestCurrencyDenomination)
        {
            return MaxPrice(maxPriceInLowestCurrencyDenomination)
                  .MinPrice(minPriceInLowestCurrencyDenomination);
        }


        private new AmazonItemSearchOperation AddOrReplace(string param, object value)
        {
            base.AddOrReplace(param, value);
            return this;
        }
    }
}