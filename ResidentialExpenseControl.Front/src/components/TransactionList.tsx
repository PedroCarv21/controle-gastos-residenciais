import type { Transaction } from "../types/transaction";
import TransactionItem from "./TransactionItem";
import "./TransactionList.css";

interface Props {
    transactions: Transaction[];
    onTransactionDeleted: (id: string) => void | Promise<void>;
}

export default function TransactionList({ transactions, onTransactionDeleted }: Props) {

    return (
        <div className="transaction-list">
            <h2>Transações</h2>
            <ul>
                {transactions.map(transaction => (
                    <TransactionItem
                        key={transaction.id}
                        transaction={transaction}
                        onDelete={onTransactionDeleted}
                    />
                ))}
            </ul>
        </div>
    );
}