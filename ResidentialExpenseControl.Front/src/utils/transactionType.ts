export function getTransactionTypeName(type: number) {
    return type === 0 ? "Despesa" : "Receita";
}