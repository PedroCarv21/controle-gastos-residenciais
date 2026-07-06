import type { Person } from "../types/person";
import PersonItem from "./PersonItem";
import "./PersonList.css";

interface PersonListProps {
    people: Person[];
    onPersonDeleted: (id: string) => void | Promise<void>;
}

export default function PersonList({
    people,
    onPersonDeleted
}: PersonListProps) {

    return (
        <div className="person-list">
        <h2>Pessoas</h2>

        <ul>
            {people.map(person => (
                <PersonItem
                    key={person.id}
                    person={person}
                    onDelete={onPersonDeleted}
                />
            ))}
        </ul>
    </div>
    );
}