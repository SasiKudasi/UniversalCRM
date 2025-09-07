"use client"
import { useState } from "react";
import { Container, Form } from "react-bootstrap";
import { CreateChildPageForm } from "../components/createChildPage";
import { CreateRootPageForm } from "../components/createRootPage";
export default function CreatePage() {
  const [isRoot, setIsRoot] = useState(true);
  return (
    <Container>
      <h2>Создание новой страницы</h2>

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
    </Container>
  );
}