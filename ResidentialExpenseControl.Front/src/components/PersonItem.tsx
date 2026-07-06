    import type { Person } from "../types/person";

    interface PersonItemProps {
        person: Person;
        onDelete: (id: string) => void;
    }

    export default function PersonItem({
        person,
        onDelete
    }: PersonItemProps) {

        return (
            <li>
                {person.name} ({person.age} anos)

                <button
                    onClick={() => onDelete(person.id)}
                    className="delete-button">
                    Excluir
                </button>
            </li>
        );
    }