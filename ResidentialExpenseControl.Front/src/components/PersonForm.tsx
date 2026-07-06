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
    onSubmit
}: PersonFormProps) {

    return (
        <>
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

            <button onClick={onSubmit}>
                Cadastrar
            </button>
        </>
    );
}