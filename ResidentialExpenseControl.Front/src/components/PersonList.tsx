import type { Person } from "../types/person";
import PersonItem from "./PersonItem";

interface PersonListProps {
    people: Person[];
    onDelete: (id: string) => void;
}

export default function PersonList({
    people,
    onDelete
}: PersonListProps) {

    return (
        <>
            <h2>Pessoas</h2>

            <ul>
                {people.map(person => (
                    <PersonItem
                        key={person.id}
                        person={person}
                        onDelete={onDelete}
                    />
                ))}
            </ul>
        </>
    );
}