import { useState } from "react";
import { createTransaction } from "../api/transactionApi";
import type { Person } from "../types/person";
import "./TransactionForm.css";
import axios from "axios";

interface Props {
    people: Person[];
    onTransactionCreated: () => void;
}



export default function TransactionForm({
    people,
    onTransactionCreated
}: Props) {

    const [description, setDescription] = useState("");
    const [value, setValue] = useState("");
    const [type, setType] = useState(0);
    const [personId, setPersonId] = useState("");

    async function handleSubmit(event: React.FormEvent) {
        event.preventDefault();
    
        try {
            await createTransaction({
                description,
                value: Number(value),
                type,
                personId
            });
    
            setDescription("");
            setValue("");
            setType(0);
            setPersonId("");
    
            onTransactionCreated();
    
        } catch (error) {
    
            if (axios.isAxiosError(error)) {
                alert(error.response?.data.message ?? "Não foi possível cadastrar a transação.");
                return;
            }
    
            alert("Erro inesperado.");
        }
    }

    return (
        <>
            <div className="transaction-card">
            <h2>Nova Transação</h2>

            <form onSubmit={handleSubmit} className="transaction-form">

                <select
                    value={personId}
                    onChange={(event) => setPersonId(event.target.value)}
                    required
                >
                    <option value="">
                        Selecione uma pessoa
                    </option>

                    {people.map(person => (

                        <option
                            key={person.id}
                            value={person.id}
                        >
                            {person.name}
                        </option>

                    ))}

                </select>


                <input
                    type="text"
                    placeholder="Descrição"
                    value={description}
                    onChange={(event) => setDescription(event.target.value)}
                    required
                />


                <input
                    type="number"
                    placeholder="Valor"
                    step="0.01"
                    value={value}
                    onChange={(event) => setValue(event.target.value)}
                    required
                />


                <select
                    value={type}
                    onChange={(event) => setType(Number(event.target.value))}
                >
                    <option value={1}>
                        Receita
                    </option>

                    <option value={0}>
                        Despesa
                    </option>

                </select>


                <button type="submit">
                    Cadastrar
                </button>

            </form>
            </div>
        </>
    );
}