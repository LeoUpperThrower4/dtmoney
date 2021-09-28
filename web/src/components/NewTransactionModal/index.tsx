import styles from './styles.module.scss'

export function NewTransactionModal() {


  return (
    <form className={styles.form}>
      <h1>Cadastrar transação</h1>
      <input type="text" name="Title" id="title" placeholder="Título da transação" />
      <input type="number" name="Amount" id="amount" placeholder="Valor da transação" />
      <label htmlFor="type">Tipo de transação</label>
      <input type="radio" name="Credit" id="type" placeholder="Crédito" />
      <input type="radio" name="Debit" id="type" placeholder="Débito" />
      <input type="text" name="Category" id="category" placeholder="Categoria da trnsação" />
    </form>
  )
}