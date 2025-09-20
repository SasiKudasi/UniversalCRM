"use client"
import { useState } from "react";
import { Col, Container, Form, Row } from "react-bootstrap";
import AdminHeader from "../components/adminHeader";
import { CreateChildPageForm } from "../components/createChildPage";
import { CreateRootPageForm } from "../components/createRootPage";
export default function CreatePage() {
  const [isRoot, setIsRoot] = useState(true);
  return (
    <Container>
      <h2>Создание новой страницы</h2>
      <Row>
        <Col sm={8}>
          <Form>
            <Form.Check
              type="switch"
              label="Корневая страница? (будет отображаться на главной)"
              checked={isRoot}
              onChange={(e) => setIsRoot(e.target.checked)}
            ></Form.Check>
            <br />
          </Form>
          {isRoot ? <CreateRootPageForm /> : <CreateChildPageForm />}
        </Col>
        <AdminHeader />
      </Row>
    </Container>
  );
}