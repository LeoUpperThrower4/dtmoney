import { useRouter } from "next/router";
import { FormEvent, ReactNode, useState } from "react";

import styles from './Home.module.scss'

export default function Home(): ReactNode {

  const [id, setId] = useState('')
  const router = useRouter();

  function handleLoginSubmit(event: FormEvent) {
    event.preventDefault()

    router.push(`/dashboard/${id}`)

  }

  return (
    <main className={styles.main}>
      <form className={styles.form} onSubmit={handleLoginSubmit}>
        <label htmlFor="id">Digite sua ID:</label>
        <input required id="id" type="text" onChange={(e) => setId(e.target.value)} />
        <button className={styles.SignInButton} type='submit'>Login</button>
      </form>
    </main>
  )
}