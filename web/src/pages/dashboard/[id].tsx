import type { NextPage } from 'next'
import Head from 'next/head'
import { Container } from './styles'
import { Header } from '../../components/Header'
import { Dashboard } from '../../components/Dashboard'
import { FaunaInfoProvider } from '../../hooks/useFaunaInfo'
import { useRouter } from 'next/router'
import Modal from 'react-modal';
import { useState } from 'react'
import { NewTransactionModal } from '../../components/NewTransactionModal'

const PageDashboard: NextPage = () => {
  const router = useRouter()
  const { id } = router.query

  const [isModalOpen, setIsModalOpen] = useState(false)

  Modal.setAppElement("#__next")

  function handleModalClose() {
    setIsModalOpen(false);
  }

  return (
    <FaunaInfoProvider>
      <Header setModalOpen={setIsModalOpen} />
      <Container>
        <Head>
          <title>dt.money | {id}</title>
        </Head>
        <Dashboard userId={id as string} />
        <Modal
          isOpen={isModalOpen}
          onRequestClose={handleModalClose}
        >
          <NewTransactionModal />
        </Modal>
      </Container>
    </FaunaInfoProvider>
  )
}

export default PageDashboard