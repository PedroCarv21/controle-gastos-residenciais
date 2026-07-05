import { useEffect, useState } from "react";
import type { Person } from "../types/person";
import { createPerson, deletePerson, getPeople } from "../api/personApi";

export default function Home() {
  const [people, setPeople] = useState<Person[]>([]);
  const [name, setName] = useState("");
  const [age, setAge] = useState("");

  useEffect(() => {
    loadPeople();
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

  return (
    <>
      <h1>Controle de Gastos Residenciais</h1>

      <div>
        <input
          type="text"
          placeholder="Nome"
          value={name}
          onChange={(e) => setName(e.target.value)}
        />

        <input
          type="number"
          placeholder="Idade"
          value={age}
          onChange={(e) => setAge(e.target.value)}
        />

        <button onClick={handleCreatePerson}>Cadastrar</button>
        <h2>Pessoas</h2>

        <ul>
          {people.map((person) => (
            <li key={person.id}>
              {person.name} ({person.age} anos)
              <button
                onClick={() => handleDeletePerson(person.id)}
                style={{ marginLeft: "10px" }}
              >
                Excluir
              </button>
            </li>
          ))}
        </ul>
      </div>
    </>
  );
}
