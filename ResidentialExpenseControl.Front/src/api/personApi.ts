import api from "./axios";
import type { Person } from "../types/person";

export async function getPeople(): Promise<Person[]> {
    const response = await api.get<Person[]>("/people");

    return response.data;
}