import "./PersonForm.css";

interface PersonFormProps {
  name: string;
  age: string;
  onNameChange: (value: string) => void;
  onAgeChange: (value: string) => void;
  onSubmit: () => void;
}

export default function PersonForm({
  name,
  age,
  onNameChange,
  onAgeChange,
  onSubmit,
}: PersonFormProps) {
  return (
    <div className="person-card">
      <h2>Nova Pessoa</h2>
      <form
        className="person-form"
        onSubmit={(e) => {
          e.preventDefault();
          onSubmit();
        }}
      >
        <input
          type="text"
          placeholder="Nome"
          value={name}
          onChange={(e) => onNameChange(e.target.value)}
        />

        <input
          type="number"
          placeholder="Idade"
          value={age}
          onChange={(e) => onAgeChange(e.target.value)}
        />

        <button type="submit">Cadastrar</button>
      </form>
    </div>
  );
}
