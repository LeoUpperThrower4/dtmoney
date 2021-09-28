import { MouseEventHandler } from 'react'
import { FiArrowUpCircle, FiArrowDownCircle } from 'react-icons/fi'

import styles from './styles.module.scss'

export function NewTransactionModal() {

  function handleChangeTransactionType(e: any) {
    console.log(e);

  }
  return (
    <form className={styles.form}>
      <h1>Cadastrar transação</h1>
      <input type="text" name="Title" id="title" placeholder="Título da transação" />
      <input type="number" name="Amount" id="amount" placeholder="Valor da transação" />
      <div className={styles.radio}>
        <button onClick={handleChangeTransactionType}>
          <FiArrowUpCircle color="#12A454" />
          Entrada
        </button>
        <button onClick={handleChangeTransactionType}>
          <FiArrowDownCircle color="#E52E4D" />
          Saída
        </button>
      </div>
      <input type="text" name="Category" id="category" placeholder="Categoria da transação" />
    </form>
  )
}