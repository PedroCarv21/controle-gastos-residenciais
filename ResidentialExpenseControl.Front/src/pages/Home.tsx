import { useEffect, useState } from "react";
import type { Person } from "../types/person";
import { createPerson, deletePerson, getPeople } from "../api/personApi";
import PersonForm from "../components/PersonForm";
import PersonList from "../components/PersonList";
import type { Transaction } from "../types/transaction";
import TransactionList from "../components/TransactionList";
import { getTransactions } from "../api/transactionApi";
import TransactionForm from "../components/TransactionForm";
import "./Home.css";
import Summary from "../components/Summary";
import axios from "axios";

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

    try {
      await createPerson({
        name: name.trim(),
        age: Number(age),
      });

      setName("");
      setAge("");

      await loadPeople();
    } catch (error) {
      if (axios.isAxiosError(error)) {
        alert(error.response?.data.message);
        return;
      }

      alert("Erro inesperado.");
    }
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
    <div className="home">
      <h1>Controle de Gastos Residenciais</h1>

      <Summary transactions={transactions} />

      <div className="forms">
        <PersonForm
          name={name}
          age={age}
          onNameChange={setName}
          onAgeChange={setAge}
          onSubmit={handleCreatePerson}
        />

        <TransactionForm
          people={people}
          onTransactionCreated={loadTransactions}
        />
      </div>

      <div className="lists">
        <PersonList people={people} onPersonDeleted={handleDeletePerson} />

        <TransactionList transactions={transactions} />
      </div>
    </div>
  );
}
