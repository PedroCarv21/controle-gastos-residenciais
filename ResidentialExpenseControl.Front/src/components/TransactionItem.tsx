import type { Transaction } from "../types/transaction";
import { getTransactionTypeName } from "../utils/transactionType";

interface Props {
  transaction: Transaction;
  onDelete: (id: string) => void;
}

export default function TransactionItem({ transaction, onDelete }: Props) {
  return (
    <li>
      <strong>{transaction.description}</strong>
      <br />
      {transaction.personName}
      <br />
      {getTransactionTypeName(transaction.type)}
      <br />
      R$ {transaction.value.toFixed(2)}
      <button
        className="delete-button"
        onClick={() => onDelete(transaction.id)}>
        Excluir
      </button>
    </li>
  );
}
