import { render, screen, fireEvent } from "@testing-library/react";
import "@testing-library/jest-dom";
import Home from "../app/page";

describe("Setup Test - Quiz Capitales", () => {
  it("renders the setup test page", () => {
    render(<Home />);

    expect(
      screen.getByText(/Quiz des Capitales - Setup Test/i)
    ).toBeInTheDocument();
  });

  it("displays all technology checks", () => {
    render(<Home />);

    expect(screen.getByText(/Next.js 15 App Router/i)).toBeInTheDocument();
    expect(screen.getByText(/Material UI v6/i)).toBeInTheDocument();
    expect(screen.getByText(/React 19 Hooks/i)).toBeInTheDocument();
    expect(screen.getByText(/TypeScript 5/i)).toBeInTheDocument();
  });

  it("increments counter when button is clicked", () => {
    render(<Home />);

    const button = screen.getByRole("button", { name: /Tester useState/i });

    // Initial state
    expect(
      screen.getByText(/useState fonctionne : 0 clics/i)
    ).toBeInTheDocument();

    // Click once
    fireEvent.click(button);
    expect(
      screen.getByText(/useState fonctionne : 1 clics/i)
    ).toBeInTheDocument();

    // Click twice
    fireEvent.click(button);
    expect(
      screen.getByText(/useState fonctionne : 2 clics/i)
    ).toBeInTheDocument();
  });

  it("shows success message", () => {
    render(<Home />);

    expect(screen.getByText(/Tout est prêt/i)).toBeInTheDocument();
    expect(
      screen.getByText(/Vous pouvez commencer l'exercice/i)
    ).toBeInTheDocument();
  });
});
