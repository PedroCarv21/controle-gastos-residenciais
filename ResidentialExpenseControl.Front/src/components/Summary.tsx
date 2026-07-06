import type { Transaction } from "../types/transaction";
import './Summary.css';

interface SummaryProps {
    transactions: Transaction[];
}

export default function Summary({
    transactions
}: SummaryProps) {

    const totalIncome = transactions
        .filter(t => t.type === 0)
        .reduce((sum, t) => sum + t.value, 0);

    const totalExpense = transactions
        .filter(t => t.type === 1)
        .reduce((sum, t) => sum + t.value, 0);

    const balance = totalIncome - totalExpense;

    return (
        <div className="summary">

            <div className="summary-card income">
                <h3>Receitas</h3>
                <p>
                    R$ {totalIncome.toFixed(2)}
                </p>
            </div>

            <div className="summary-card expense">
                <h3>Despesas</h3>
                <p>
                    R$ {totalExpense.toFixed(2)}
                </p>
            </div>

            <div className="summary-card balance">
                <h3>Saldo</h3>
                <p>
                    R$ {balance.toFixed(2)}
                </p>
            </div>

        </div>
    );
}