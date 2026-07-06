import type { Transaction } from "../types/transaction";
import { getTransactionTypeName } from "../utils/transactionType";

interface Props {
    transaction: Transaction;
}

export default function TransactionItem({ transaction }: Props) {

    return (
        <li>
            <strong>{transaction.description}</strong>
            <br/>
            {transaction.personName}
            <br/>
            {getTransactionTypeName(transaction.type)}
            <br/>
            R$ {transaction.value.toFixed(2)}
        </li>
    );
}