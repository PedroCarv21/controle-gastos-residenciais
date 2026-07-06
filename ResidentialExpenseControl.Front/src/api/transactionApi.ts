import api from "./axios";
import type { Transaction } from "../types/transaction";

export async function getTransactions() {
    const response = await api.get<Transaction[]>("/transactions");
    return response.data;
}