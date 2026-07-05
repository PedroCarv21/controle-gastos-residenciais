export enum TransactionType {
    Expense = 0,
    Income = 1
}

export interface Transaction {
    id: string;
    description: string;
    value: number;
    type: TransactionType;
    personId: string;
}