import api from "./axios";
import type { Transaction } from "../types/transaction";

export async function getTransactions() {
    const response = await api.get<Transaction[]>("/transactions");
    return response.data;
}

export interface CreateTransactionDTO {
    description: string;
    value: number;
    type: number;
    personId: string;
}

export async function createTransaction(
    dto: CreateTransactionDTO
): Promise<Transaction> {

    const response = await api.post<Transaction>(
        "/transactions",
        dto
    );

    return response.data;
}

export async function deleteTransaction(id: string) {
    await api.delete(`/transactions/${id}`);
}