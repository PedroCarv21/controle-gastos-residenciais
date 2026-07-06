import type { Transaction } from "../types/transaction";
import TransactionItem from "./TransactionItem";

interface Props {
    transactions: Transaction[];
}

export default function TransactionList({ transactions }: Props) {

    return (
        <>
            <h2>Transações</h2>
            <ul>
                {transactions.map(transaction => (
                    <TransactionItem
                        key={transaction.id}
                        transaction={transaction}
                    />
                ))}
            </ul>
        </>
    );
}