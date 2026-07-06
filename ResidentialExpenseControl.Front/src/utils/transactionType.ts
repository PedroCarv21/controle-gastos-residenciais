export function getTransactionTypeName(type: number): string {

    switch (type) {

        case 0:
            return "Receita";

        case 1:
            return "Despesa";

        default:
            return "Desconhecido";
    }

}