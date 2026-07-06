import { useEffect, useState } from "react";
import type { Person } from "../types/person";
import { createPerson, deletePerson, getPeople } from "../api/personApi";
import PersonForm from "../components/PersonForm";
import PersonList from "../components/PersonList";
import type { Transaction } from "../types/transaction";
import TransactionList from "../components/TransactionList";
import { getTransactions } from "../api/transactionApi";
import TransactionForm from "../components/TransactionForm";

export default function Home() {
  const [people, setPeople] = useState<Person[]>([]);
  const [name, setName] = useState("");
  const [age, setAge] = useState("");
  const [transactions, setTransactions] = useState<Transaction[]>([]);

  useEffect(() => {
    loadPeople();
    loadTransactions();
  }, []);

  async function loadPeople() {
    const data = await getPeople();
    setPeople(data);
  }

  async function handleCreatePerson() {
    if (!name.trim()) {
      alert("Informe um nome.");
      return;
    }

    if (!age) {
      alert("Informe uma idade.");
      return;
    }

    await createPerson({
      name: name.trim(),
      age: Number(age),
    });

    setName("");
    setAge("");

    await loadPeople();
  }

  async function handleDeletePerson(id: string) {
    const confirmed = confirm("Deseja realmente excluir esta pessoa?");

    if (!confirmed) {
      return;
    }

    await deletePerson(id);

    await loadPeople();
  }

  async function loadTransactions() {
    const data = await getTransactions();
    setTransactions(data);
  }
  return (
    <>
      <h1>Controle de Gastos Residenciais</h1>

      <PersonForm
        name={name}
        age={age}
        onNameChange={setName}
        onAgeChange={setAge}
        onSubmit={handleCreatePerson}
      />

      <PersonList people={people} onDelete={handleDeletePerson} />

      <TransactionForm
        people={people}
        onTransactionCreated={loadTransactions}
      />
      <TransactionList transactions={transactions} />
    </>
  );
}
