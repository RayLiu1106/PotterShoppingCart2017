﻿using System.Collections.Generic;
using System.Linq;

namespace PotterShoppingCartTests
{
    internal class Cart
    {
        private const int price = 100;

        private Dictionary<int, decimal> _discount = new Dictionary<int, decimal>()
        {
            {0, 0},
            {1, 1},
            {2, 0.95m},
            {3, 0.9m},
            {4, 0.8m },
            {5, 0.75m },
        };

        public Cart()
        {
        }

        public decimal Checkout(IEnumerable<Book> books)
        {
            return CheckoutBySuites(books, 0);
        }

        private decimal CheckoutBySuites(IEnumerable<Book> books, decimal amount)
        {
            if (!books.Any())
            {
                return amount;
            }

            var suite = books.GroupBy(b => b.ISBN).Select(x => x.First());
            var totalAmount = amount + AmountOfEachSuite(suite);
            var unSuiteBooks = books.Except(suite);
            return CheckoutBySuites(unSuiteBooks, totalAmount);
        }

        private decimal AmountOfEachSuite(IEnumerable<Book> suiteOfBooks)
        {
            decimal amount = suiteOfBooks.Sum(x => price);
            var discount = GetSuiteDiscount(suiteOfBooks);

            return amount * discount;
        }

        private decimal GetSuiteDiscount(IEnumerable<Book> books)
        {
            var count = books.Count();
            return this._discount[count];
        }
    }
}