import api from "./axios";
import type { Person } from "../types/person";
import type { PersonRequest } from "../types/personRequest";

export async function getPeople(): Promise<Person[]> {
    const response = await api.get<Person[]>("/people");

    return response.data;
}

export async function createPerson(person: PersonRequest) {
    const response = await api.post<Person>("/people", person);
    return response.data;
}