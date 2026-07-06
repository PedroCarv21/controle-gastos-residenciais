import { useState } from "react";
import { createTransaction } from "../api/transactionApi";
import type { Person } from "../types/person";

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
    }

    return (
        <>
            <h2>Nova Transação</h2>

            <form onSubmit={handleSubmit}>

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

                <br /><br />

                <input
                    type="text"
                    placeholder="Descrição"
                    value={description}
                    onChange={(event) => setDescription(event.target.value)}
                    required
                />

                <br /><br />

                <input
                    type="number"
                    placeholder="Valor"
                    step="0.01"
                    value={value}
                    onChange={(event) => setValue(event.target.value)}
                    required
                />

                <br /><br />

                <select
                    value={type}
                    onChange={(event) => setType(Number(event.target.value))}
                >
                    <option value={0}>
                        Receita
                    </option>

                    <option value={1}>
                        Despesa
                    </option>

                </select>

                <br /><br />

                <button type="submit">
                    Cadastrar
                </button>

            </form>
        </>
    );
}