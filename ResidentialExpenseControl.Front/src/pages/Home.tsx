import { useEffect } from "react";
import { getPeople } from "../api/personApi";

function Home() {

    useEffect(() => {

        async function loadPeople() {

            try {

                const people = await getPeople();

                console.log(people);

            } catch (error) {

                console.error(error);

            }

        }

        loadPeople();

    }, []);

    return (
        <h1>Residential Expense Control</h1>
    );
}

export default Home;