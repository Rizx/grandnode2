﻿using Grand.Business.Core.Queries.Checkout.Orders;
using Grand.Domain.Payments;
using MediatR;

namespace Grand.Business.Checkout.Queries.Handlers.Orders
{
    public class CanMarkPaymentTransactionAsPaidQueryHandler : IRequestHandler<CanMarkPaymentTransactionAsPaidQuery, bool>
    {
        public async Task<bool> Handle(CanMarkPaymentTransactionAsPaidQuery request, CancellationToken cancellationToken)
        {
            var paymentTransaction = request.PaymentTransaction;
            if (paymentTransaction == null)
                throw new ArgumentNullException(nameof(request.PaymentTransaction));

            if (
                paymentTransaction.TransactionStatus == TransactionStatus.Canceled ||
                paymentTransaction.TransactionStatus == TransactionStatus.Paid ||                
                paymentTransaction.TransactionStatus == TransactionStatus.Refunded ||
                paymentTransaction.TransactionStatus == TransactionStatus.PartiallyRefunded ||
                paymentTransaction.TransactionStatus == TransactionStatus.Voided)
                return false;

            return await Task.FromResult(true);
        }
    }
}
